import { register, init, getLocaleFromNavigator, locale } from 'svelte-i18n';
import { browser } from '$app/environment';

register('en', () => import('./locales/en.json'));
register('pt', () => import('./locales/pt.json'));


export async function setupI18n() {
    if (browser) {
        await init({
            fallbackLocale: 'en',
            initialLocale: 'en' //getLocaleFromNavigator(),
        });
    }
}