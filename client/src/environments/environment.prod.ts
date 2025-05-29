export const environment = {
  production: true,
  version: '1.0.0',
  baseUrlApi: 'http://localhost:8080',
  tokenWhitelistedDomains: [
    new RegExp('localhost:8080'),
  ],
  tokenBlacklistedRoutes: [
    new RegExp('\/v1\/api\/token'),
    new RegExp('\/v1\/api\/customer\/lead'),
  ]
};
