export const environment = {
  production: true,
  version: '1.0.0',
  baseUrlApi: 'http://localhost:80',
  tokenWhitelistedDomains: [
    new RegExp('localhost:80'),
  ],
  tokenBlacklistedRoutes: [
    new RegExp('\/v1\/api\/token'),
    new RegExp('\/v1\/api\/customer\/lead'),
  ]
};
