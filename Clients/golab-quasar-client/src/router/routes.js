
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/IndexPage.vue'), name: 'Strona Główna' },
      { path: '/callback', component: () => import('pages/oidc/callback.vue'), name: "Przekierowanie po Logowaniu"},
      { path: '/about', component: () => import('pages/AboutPage.vue'), name: 'Informacje o Aplikacji' },
      {path: '/editprofile', component: () => import('pages/profile/UserProfileEditPage.vue'), name: "Edycja Profilu"},
      {path: '/profile/:userHandle', component: () => import('pages/profile/UserProfilePage.vue'), name: "Profil Użytkownika"}
    ]
  },


  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
