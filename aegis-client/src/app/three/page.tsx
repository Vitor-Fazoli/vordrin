'use client';

import { useFirstStepsStore } from '../../../store/useFirstStepsStore';
import { useRouter } from 'next/navigation';
import Image from 'next/image'
import { API_URL } from '@/app/layout';

export default function ChooseWeapon() {
  const { backgroundId, countryId } = useFirstStepsStore();
  const router = useRouter();

  async function CompleteCharacter(weaponId :number){
    const response = await fetch(`${API_URL}/character/new`, {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
              character: 10,
              weapon: weaponId,
              background: backgroundId,
              country: countryId,
            })
          });
  }

  const weapons = [
    { id: 1, name: 'Greatsword', icon: '/sword-alt.png', description : 'A massive blade wielded by those who stand firm against all odds. Its weight makes it slow, but every strike commands attention.', details: 'Best for players who like methodical combat and tanking damage.' },
    { id: 2, name: 'Longbow', icon: '/bow-arrow.png', description : 'A precise and disciplined weapon. Each shot requires preparation, making it deadly in the hands of a patient hunter.', details:'Best for players who excel at positioning and timing their attacks.' },
    { id: 3, name: 'Purger', icon:'/syringe.png', description : 'A tool of both destruction and salvation. Each strike weakens the enemy, but every 30 hits, the wielder can turn the tide by saving an ally.' , details:'Best for players who balance aggression with team support.' },
  ];

  return (
    <div className="flex flex-col items-center space-y-4 p-6 h-screen">
      <h1 className="text-2xl font-bold h-1/5">Choose your weapon </h1>
      <div className='flex gap-4 w-full h-3/5 justify-center items-center'>
      {weapons.map((w) => (
        <div className='flex flex-col border border-rose-700 p-5 w-1/3 h-4/5 gap-2 justify-between'>
            <div className='flex flex-col gap-4'>
            <div className='w-full flex items-center justify-center'>
                <div className='bg-rose-700 p-1 outline outline-rose-700 outline-offset-3'>
                <Image
                  src={w.icon}
                  width={25}
                  height={25}
                  alt={w.name}
                />
                </div>
              </div>
              <h1 className='w-full text-center text-2xl'>{w.name}</h1>
              <div>
                <p>
                {w.description}
                </p>
                <p>
                  {w.details}
                </p>
              </div>
            </div>
            <button
          key={w.id}
          className={`px-4 py-2 border border-rose-700 hover:cursor-pointer hover:bg-rose-700 duration-150`}
          onClick={() => {
            CompleteCharacter(w.id)
            router.push('/game')
          }}
        >
          Select
        </button>
        </div>
      ))}
      </div>
    </div>
  );
}
