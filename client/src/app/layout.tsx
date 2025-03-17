'use client'

import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import { initializeApp } from "firebase/app";
import "./globals.css";
import { getAuth, onAuthStateChanged, User, signOut } from "firebase/auth";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { Nova_Square } from 'next/font/google';

const novaSquare = Nova_Square({
  subsets: ['latin'],
  variable: '--font-nova-square',
  weight: '400',
});


const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

const metadata: Metadata = {
  title: "Codename Aegis",
  description: "An incremental clicking game",
};

const firebaseConfig = {
  apiKey: process.env.NEXT_PUBLIC_FIREBASE_API_KEY,
  authDomain: process.env.NEXT_PUBLIC_FIREBASE_AUTH_DOMAIN,
  projectId: process.env.NEXT_PUBLIC_FIREBASE_PROJECT_ID,
  storageBucket: process.env.NEXT_PUBLIC_FIREBASE_STORAGE_BUCKET,
  messagingSenderId: process.env.NEXT_PUBLIC_FIREBASE_MESSAGING_SENDER_ID,
  appId: process.env.NEXT_PUBLIC_FIREBASE_APP_ID,
  measurementId: process.env.NEXT_PUBLIC_FIREBASE_MEASUREMENT_ID
};

export const app = initializeApp(firebaseConfig);
export const auth = getAuth(app);

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {

  const router = useRouter();

  const signingOut = () => {
    signOut(auth);
    router.push('/sign-in');
  }

  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(auth, (user) => {
      setUser(user);
    });

    return () => unsubscribe();
  }, []);

  return (
    <html lang="en">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased w-screen h-screen`}
      >
        <header className="h-1/12 w-full flex justify-between items-center px-4 bg-black">
          <h1 className="text-3xl font-bold text-rose-700">Codename Aegis</h1>
          {user ? (
            <div className="flex gap-2">
              <button className="py-1 px-2 text-sm ring-2 ring-rose-700 text-white rounded-lg hover:bg-rose-800 transition-colors" onClick={signingOut}>Sign Out</button>
            </div>
          ) : (
            <div className="flex gap-2">
              <a href="/sign-in" className="py-1 px-2 text-sm ring-2 ring-rose-700 text-white rounded-lg hover:bg-rose-800 transition-colors">
                Sign In
              </a>
              <a href="/register" className="py-1 px-2 text-sm ring-2 ring-rose-700 text-white rounded-lg hover:bg-rose-800 transition-colors">
                Register
              </a>
            </div>
          )}
        </header>
        <div className="h-11/12 w-full">
          {children}
        </div>
      </body>
    </html>
  );
}
