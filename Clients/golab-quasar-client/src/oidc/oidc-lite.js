import Oidc from 'oidc-client'

const mgr = new Oidc.UserManager({
  userStore: new Oidc.WebStorageStateStore({
    // prefix: 'oidc',
    store: window.localStorage
  }),
  authority: "http://localhost:5000",
  client_id: 'quasarApp',
  redirect_uri: 'http://localhost:9000/#/callback#',
  response_type: 'code',
  scope: 'openid profile golab', 
  post_logout_redirect_uri: "http://localhost:9000/",
  accessTokenExpiringNotificationTime: 2,
  filterProtocolClaims: true,
  loadUserInfo: true,
  clockSkew: 900,
})


  export default mgr