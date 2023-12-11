export class KeepInVault {
    constructor(data) {
        this.keepId = data.id
        this.vaultKeepId = data.vaultKeepId
        this.accountId = data.accountId
        this.creatorId = data.creatorId
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creator = data.creator
        this.createdAt = data.createdAt
        this.updatedAt = data.updatedAt
    }
}
