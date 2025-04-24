import { register, init, getLocaleFromNavigator } from 'svelte-i18n';

register('en', () => import('./locales/en.json'));
register('pt', () => import('./locales/pt.json'));

init({
    fallbackLocale: 'en',
    initialLocale: getLocaleFromNavigator()
});
