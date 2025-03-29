'use client'

import type { Metadata } from "next";
import "./globals.css";
import { Nova_Square } from 'next/font/google';

const novaSquare = Nova_Square({
  subsets: ['latin'],
  variable: '--font-nova-square',
  weight: '400',
});

const metadata: Metadata = {
  title: "Codename Aegis",
  description: "An incremental clicking game",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
        <body className="h-screen w-screen">
          {children}
        </body>
    </html>
  );
}
