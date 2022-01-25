import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: '认证服务',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44399',
    redirectUri: baseUrl,
    clientId: 'IdentityServer_App',
    responseType: 'code',
    scope: 'offline_access IdentityServer role email openid profile',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44399',
      rootNamespace: 'Lcn.IdentityServer',
    },
    IdentityServer: {
      url: 'https://localhost:44339',
      rootNamespace: 'Lcn.IdentityServer',
    },
  },
} as Environment;
