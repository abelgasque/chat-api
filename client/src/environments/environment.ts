export const environment = {
  production: false,
  version: '1.0.0',
  baseUrlApi: 'https://localhost:8080',
  tokenWhitelistedDomains: [
    new RegExp('localhost:8080'),
  ],
  tokenBlacklistedRoutes: [
    new RegExp('\/v1\/api\/token'),
    new RegExp('\/v1\/api\/customer\/lead'),
  ]
};
