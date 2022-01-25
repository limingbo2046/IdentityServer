import { Environment } from '@abp/ng.core';

const baseUrl = 'http://10.160.19.164:9010';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://10.160.19.164:9010/',
    name: '运维管理',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://10.160.19.164:9009',
    redirectUri: baseUrl,
    clientId: 'IdentityServer_Sap',
    responseType: 'code',
    scope: 'offline_access IdentityServer',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://10.160.19.164:9009',
      rootNamespace: 'Lcn.IdentityServer',
    },
    IdentityServer: {
      url: 'https://10.160.19.164:9009',
      rootNamespace: 'Lcn.IdentityServer',
    },
  },
} as Environment;
