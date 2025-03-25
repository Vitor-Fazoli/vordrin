'use client';

import { useFirstStepsStore } from '../../../store/useFirstStepsStore';
import { useRouter } from 'next/navigation';

export default function ChooseCountry() {
  const { countryId, setCountry } = useFirstStepsStore();
  const router = useRouter();

  // IDs para cada país
  const countries = [
    { id: 1, name: 'Brazil' },
    { id: 2, name: 'United States' },
    { id: 3, name: 'Australia' },
    { id: 4, name: 'Japan' },
    { id: 5, name: 'Republic of Cameroon' },
  ];

  return (
    <div className="flex flex-col items-center space-y-4 p-6">
      <h1 className="text-2xl font-bold">Escolha seu País</h1>
      <div className='flex gap-2 w-full h-full'>
        {countries.map((c) => (
            <button
              key={c.id}
              className={`flex-grow px-4 py-2 border border-rose-700 ${countryId === c.id ? 'bg-blue-600 text-white' : ''}`}
              onClick={() => {
                setCountry(c.id);
                router.push('/first-steps/two');
              }}
            >
              {c.name}
            </button>
        ))}
      </div>
      <div>
        <button>
            Next
        </button>
      </div>
    </div>
  );
}
