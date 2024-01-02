import Oidc from 'oidc-client'

const oidc = new Oidc.UserManager({response_mode:"query", userStore: new Oidc.WebStorageStateStore()});
export default oidc;