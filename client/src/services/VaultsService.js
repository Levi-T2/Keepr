import { AppState } from "../AppState"
import { KeepInVault } from "../models/KeepInVault"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
    async GetKeepsInVault(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        const newVaultKeeps = res.data.map((vaultKeep) => new KeepInVault(vaultKeep))
        AppState.keepsInVault = newVaultKeeps
        logger.log(`[VAULT SERVICE] VaultKeeps In AppState =>`, AppState.keepsInVault)
    }
    async GetVaultById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = new Vault(res.data)
        logger.log(`[VAULT SERVICE] Vault In AppState =>`, AppState.activeVault)
    }
    async CreateVault(vaultData) {
        const res = await api.post(`api/vaults`, vaultData)
        const newVault = new Vault(res.data)
        AppState.vaults.push(newVault)
        return newVault;
    }
    async DeleteVault(vaultId) {
        const vaultIndex = AppState.vaults.findIndex((vault) => vault.id == vaultId)
        const res = await api.delete(`api/vaults/${vaultId}`)
        AppState.vaults.splice(vaultIndex, 1)
    }
    async EditVault(vaultId, editData) {
        const res = await api.put(`api/vaults/${vaultId}`, editData)
        AppState.activeVault = new Vault(res.data)
    }
}

export const vaultsService = new VaultsService()