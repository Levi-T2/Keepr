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
    async CreateKeep(keepData) {
        const res = await api.post(`api/keeps`, keepData)
        logger.log(res.data)
    }
}

export const keepsService = new KeepsService()