"use client";
import { FaGamepad, FaChartBar } from 'react-icons/fa';
import { useRouter } from 'next/navigation';

export default function GameLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const router = useRouter();

    return (
      <main className="h-full flex">
        <nav className="bg-rose-900 shadow-lg w-16 flex flex-col items-center py-4 space-y-4">
        <button 
          onClick={() => router.push('/game')}
          className="p-2 rounded-lg text-black hover:text-white"
          aria-label="Game"
        >
          <FaGamepad size={24} />
        </button>
        <button
          onClick={() => router.push('/game/stats')}
          className="p-2 rounded-lg text-black hover:text-white"
          aria-label="Stats"
        >
          <FaChartBar size={24} />
        </button>
      </nav>
        <div className="flex-1 pl-5 pt-5">
          {children}
      </div>
    </main>
  );
}