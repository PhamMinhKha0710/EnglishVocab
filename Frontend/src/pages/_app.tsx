import type { AppProps } from 'next/app';
import '@/styles/globals.css';
import { InstallPrompt } from '@/components/PWA/InstallPrompt';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <>
      <Component {...pageProps} />
      <InstallPrompt />
    </>
  )
}

export default MyApp; 