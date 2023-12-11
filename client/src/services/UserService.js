import { AppState } from "../AppState"
import { Account } from "../models/Account"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class UserService {
    async GetKeepsForUser(endpointUrl) {
        const res = await api.get(endpointUrl)
        const newKeeps = res.data.map((keep) => new Keep(keep))
        AppState.keepsForUser = newKeeps
        logger.log(`[USER SERVICE] Keeps In AppState =>`, AppState.keepsForUser)
    }
    async GetVaultsForUser(endpointUrl) {
        const res = await api.get(endpointUrl)
        const newVaults = res.data.map((vault) => new Vault(vault))
        AppState.vaults = newVaults
        logger.log(`[USER SERVICE] Vaults In AppState =>`, AppState.vaults)
    }
    async GetProfileDetails(profileId) {
        const res = await api.get(`api/profiles/${profileId}`)
        const profileData = new Account(res.data)
        AppState.profile = profileData
    }

}

export const userService = new UserService()