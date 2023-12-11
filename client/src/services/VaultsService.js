import { AppState } from "../AppState"
import { KeepInVault } from "../models/KeepInVault"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async GetVaultsForAccount() {
        const res = await api.get(`account/vaults`)
        const newVaults = res.data.map((vault) => new Vault(vault))
        AppState.vaults = newVaults
        logger.log(`[VAULTS SERVICE] Vaults In AppState =>`, AppState.vaults)
    }
    async GetKeepsInVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        const newVaultKeeps = res.data.map((vaultKeep) => new KeepInVault(vaultKeep))
        AppState.keepsInVault = newVaultKeeps
        logger.log(`[VAULT SERVICE] VaultKeeps In AppState =>`, AppState.keepsInVault)
    }
    async CreateVault(vaultData) {
        const res = await api.post(`api/vaults`, vaultData)
        const newVault = new Vault(res.data)
        AppState.vaults.push(newVault)
    }
    async DeleteVault(vaultId) {
        const vaultIndex = AppState.vaults.findIndex((vault) => vault.id == vaultId)
        const res = await api.delete(`api/vaults/${vaultId}`)
        AppState.vaults.splice(vaultIndex, 1)
    }
}

export const vaultsService = new VaultsService()