import { api } from "./AxiosService"

class VaultKeepsService {
    async CreateVaultKeep(vaultKeepData) {
        const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    }
}

export const vaultKeepsService = new VaultKeepsService()