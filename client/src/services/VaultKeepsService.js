import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepsService {
    async CreateVaultKeep(vaultKeepData) {
        const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    }
    async DeleteVaultKeep(vaultKeepId) {
        const index = AppState.keepsInVault.findIndex((keep) => keep.vaultKeepId == vaultKeepId)
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        AppState.keepsInVault.splice(index, 1)
    }
}

export const vaultKeepsService = new VaultKeepsService()