'use client';

import { useFirstStepsStore } from '../../../store/useFirstStepsStore';
import { useRouter } from 'next/navigation';

export default function CountryPage() {
  const { countryId, setCountry } = useFirstStepsStore();
  const router = useRouter();

  const countries = [
    { id: 1, name: 'Brazil' },
    { id: 2, name: 'United States' },
    { id: 3, name: 'Australia' },
    { id: 4, name: 'Japan' },
    { id: 5, name: 'Republic of Cameroon' },
  ];

  return (
    <div className="flex flex-col items-center space-y-4 p-6 h-full w-full">
      <h1 className="text-2xl font-bold">Choose your country</h1>
      <div className='flex gap-2 w-full h-full'>
        {countries.map((c) => (
        <div className='flex-1 h-full border border-rose-700 justify-between'>
          <div className='w-full text-center h-9/10 pt-10'>
            <p>{c.name}</p>
          </div>
          <div className='w-full flex justify-center items-center'>
            <button
              key={c.id}
              className={`w-1/2 px-4 py-2 border border-rose-700 hover:cursor-pointer hover:bg-rose-700 duration-150`}
              onClick={() => {
                setCountry(c.id);
                router.push('/first-steps/two');
              }}>
              Select
            </button>
          </div>
        </div>
        ))}
      </div>
    </div>
  );
}
