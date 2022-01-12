import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'IdentityServer',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44334',
    redirectUri: baseUrl,
    clientId: 'IdentityServer_App',
    responseType: 'code',
    scope: 'offline_access IdentityServer',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44334',
      rootNamespace: 'Lcn.IdentityServer',
    },
    IdentityServer: {
      url: 'https://localhost:44339',
      rootNamespace: 'Lcn.IdentityServer',
    },
  },
} as Environment;
