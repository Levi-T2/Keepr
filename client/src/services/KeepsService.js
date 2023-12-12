import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async GetKeeps() {
        const res = await api.get(`api/keeps`)
        const newKeeps = res.data.map((keep) => new Keep(keep))
        AppState.keeps = newKeeps
        logger.log(`[KEEPS SERVICE] Keeps In AppState =>`, AppState.keeps)
    }
    async GetKeepById(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = new Keep(res.data)
    }
    async CreateKeep(keepData) {
        const res = await api.post(`api/keeps`, keepData)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
    }
    async DeleteKeep(keepId) {
        const keepIndex = AppState.keeps.findIndex((keep) => keep.id == keepId)
        const res = await api.delete(`api/keeps/${keepId}`)
        AppState.keeps.splice(keepIndex, 1)
        AppState.keepsForUser.splice(keepIndex, 1)
        AppState.keepsInVault.splice(keepIndex, 1)
    }
}

export const keepsService = new KeepsService()