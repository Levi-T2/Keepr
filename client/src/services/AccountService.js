import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async GetMyVaults() {
    const res = await api.get(`account/vaults`)
    const newVaults = res.data.map((vault) => new Vault(vault))
    AppState.usersVaults = newVaults
    logger.log(`[ACCOUNT SERVICE] Vaults For User =>`, AppState.usersVaults)
  }

  async EditAccount(editData) {
    const res = await api.put(`account`, editData)
    AppState.account = new Account(res.data)
  }
}

export const accountService = new AccountService()
