'use client';

import { useFirstStepsStore } from '../../../store/useFirstStepsStore';
import { useRouter } from 'next/navigation';

export default function ChooseWeapon() {
  const { weaponId, setWeapon } = useFirstStepsStore();
  const router = useRouter();

  let weaponChoosed : number = weaponId ?? 0;

  function chooseWeapon(idWeapon :number){
    weaponChoosed = idWeapon;
  }

  // IDs para cada arma
  const weapons = [
    { id: 1, name: 'Greatsword', description : 'A massive blade wielded by those who stand firm against all odds. Its weight makes it slow, but every strike commands attention.', details: 'Best for players who like methodical combat and tanking damage.' },
    { id: 2, name: 'Longbow', description : 'A precise and disciplined weapon. Each shot requires preparation, making it deadly in the hands of a patient hunter.', details:'Best for players who excel at positioning and timing their attacks.' },
    { id: 3, name: 'Purger', description : 'A tool of both destruction and salvation. Each strike weakens the enemy, but every 30 hits, the wielder can turn the tide by saving an ally.' , details:'Best for players who balance aggression with team support.' },
  ];

  return (
    <div className="flex flex-col items-center space-y-4 p-6 h-screen">
      <h1 className="text-2xl font-bold h-1/5">Escolha sua Arma</h1>
      <div className='flex gap-4 w-full h-3/5 justify-center items-center'>
      {weapons.map((w) => (
        <div className='flex flex-col border border-rose-700 p-5 w-1/3 h-4/5 gap-2 justify-between'>
            <h1 className='w-full text-center'>{w.name}</h1>
            <p className='italic'>
            {w.description}
            </p>
            <p>{w.details}</p>
            <button
          key={w.id}
          className={`px-4 py-2 border border-rose-700 hover:cursor-pointer ${weaponId === w.id ? 'bg-rose-700 text-white' : ''}`}
          onClick={() => {
            chooseWeapon(w.id)
          }}
        >
          Select
        </button>
        </div>
      ))}
      </div>
      <div className='w-full flex justify-between'>
      <button
            className="mt-4 px-4 py-2 border border-rose-700 hover:cursor-pointer hover:bg-rose-700"
            onClick={() => router.push('/first-steps/two')}
        >
            Back
        </button>
        <button className="mt-4 px-4 py-2 border border-rose-700 hover:cursor-pointer hover:bg-rose-700" onClick={() => 
            {
                if (!weaponId) {
                    return;
                }

                setWeapon(weaponChoosed);
                router.push('/game');
            }
        }>
            Create
        </button>
      </div>
    </div>
  );
}
