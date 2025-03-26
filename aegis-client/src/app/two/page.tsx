'use client';

import { useFirstStepsStore } from '../../../store/useFirstStepsStore';
import { useRouter } from 'next/navigation';

export default function BackgroundPage() {
  const { countryId, backgroundId, setBackground } = useFirstStepsStore() as { countryId: number; backgroundId: number; setBackground: (id: number) => void };
  const router = useRouter();

  if (!countryId) {
    router.push('/first-steps/one');
    return null;
  }

  let choosedBackground : number = backgroundId ?? 0;

  function chooseBackground(backgroundId : number){
    choosedBackground = backgroundId;
  }

  // Passados filtrados pelo país
  const backgroundsByCountry: { [key: number]: { id: number; name: string; description: string; buffs: string[]; debuffs: string[] }[] } = {
        "1": [
          {
            "id": 1,
            "name": "Survivor of the Margins",
            "description": "You grew up in the alleys, learning to survive in the urban chaos. Adaptable and clever, you always find a way out.",
            "buffs": [
              "+10% dodge time",
              "+5% of seals found on enemies"
            ],
            "debuffs": [
              "Armor protects 5% less"
            ]
          },
          {
            "id": 2,
            "name": "Son of Earth",
            "description": "Raised in the countryside, you know the fauna and flora well, learning to deal with the unexpected. Your resilience is impressive.",
            "buffs": [
              "+10% RES against poisons and bleeding",
              "Recovers +1% HP every 5 seconds"
            ],
            "debuffs": [
              "Your clicks deal 5% less damage"
            ]
          },
          {
            "id": 3,
            "name": "Master Politician",
            "description": "You grew up learning the art of negotiation and the political game. You know how to influence allies and manipulate situations in your favor.",
            "buffs": [
              "-10% cooldown on dodge",
              "+5% damage when playing solo"
            ],
            "debuffs": [
                "+5% damage when playing solo"
            ]
          }
        ],
        "2": [
          {
            "id": 1,
            "name": "Descendant of the Pioneers",
            "description": "Your family has always thrived on exploring the unknown. You carry that resilient spirit.",
            "buffs": [
              "-10% cooldown on dodge",
              "+5% damage when playing solo"
            ],
            "debuffs": [
              "-5% XP when playing in a group"
            ]
          },
          {
            "id": 2,
            "name": "Corporate Oppression",
            "description": "Grew up in an environment where large corporations dictate the rules, forcing you to turn against a system that has never been on your side.",
            "buffs": [],
            "debuffs": [
              "Equipment costs 10% more to buy"
            ]
          },
          {
            "id": 3,
            "name": "Fire and Steel",
            "description": "He grew up around weapons and trained from an early age. His handling of weapons is impeccable.",
            "buffs": [
              "+5% click damage with melee weapons",
              "+1% armor for each heavy armor"
            ],
            "debuffs": [
              "-10% maximum life"
            ]
          }
        ],
        "3": [
          {
            "id": 1,
            "name": "Son of the Outback",
            "description": "You grew up surrounded by natural dangers. Now, your endurance and tracking skills are second to none.",
            "buffs": [
              "+5% resistance to poisons and burns",
              "+10% damage against large enemies"
            ],
            "debuffs": [
              "Healing potions restore 10% less HP"
            ]
          },
          {
            "id": 2,
            "name": "Snake Hunter",
            "description": "You've learned to live with and fight lethal creatures. Your keen eye avoids traps.",
            "buffs": [
              "10% less chance of being hit by surprise attacks",
              "Extra damage against poisonous enemies"
            ],
            "debuffs": [
              "Receives 5% more damage from mechanical enemies"
            ]
          },
          {
            "id": 3,
            "name": "Ally of the Tides",
            "description": "Raised on the coast, you've become accustomed to mastering the unpredictable. Your agility is impressive, but your impulsiveness comes at a price.",
            "buffs": [
              "+10% movement speed",
              "+5% stun resistance"
            ],
            "debuffs": [
              "Gains 10% less XP in defense skills"
            ]
          }
        ],
        "4": [
          {
            "id": 1,
            "name": "Disciple of Tradition",
            "description": "From an early age, you trained with discipline and rigor. Your focus turns simple attacks into lethal blows.",
            "buffs": [
              "+15% damage when attacking at the right time",
              "+5% attack speed with blades"
            ],
            "debuffs": [
              "-5% physical resistance"
            ]
          },
          {
            "id": 2,
            "name": "Forged in Honor",
            "description": "Your name carries a legacy. You fight with pride and never run away from a duel.",
            "buffs": [
              "+10% damage in 1v1 battles",
              "+5% resistance to fear and panic"
            ],
            "debuffs": [
              "10% evasion penalty (you face dangers head on)"
            ]
          },
          {
            "id": 3,
            "name": "Silent Shadow",
            "description": "You've learned that shadows are safer than light. Quick, sharp attacks are your trademark.",
            "buffs": [
              "+10% damage from stealth attacks",
              "+5% critical hit chance when attacking from behind"
            ],
            "debuffs": [
              "-5% defense when attacked from the front"
            ]
          }
        ],
        "5": [
          {
            "id": 1,
            "name": "Indomitable Spirit",
            "description": "Raised in the midst of your people's warrior culture, you've learned to resist and keep fighting, no matter the situation.",
            "buffs": [
              "+10% resistance to negative effects (paralysis, fear, slowness)",
              "+5% damage when fighting alongside allies"
            ],
            "debuffs": [
              "-5% efficiency in ranged attacks"
            ]
          },
          {
            "id": 2,
            "name": "Wild Scout",
            "description": "Your training on the savannah has honed your senses. You always know when danger is near.",
            "buffs": [
              "+10% speed when dodging",
              "+5% damage against fleeing targets"
            ],
            "debuffs": [
              "5% reduction in defense when surrounded"
            ]
          },
          {
            "id": 3,
            "name": "Guardian of Tradition",
            "description": "You grew up listening to the stories of great warriors and learned that collectivity is the greatest strength.",
            "buffs": [
              "+10% maximum life",
              "+5% resistance to elemental attacks"
            ],
            "debuffs": [
              "Magic items cost 10% more gold to buy"
            ]
          }
        ]
    };

  const backgrounds = backgroundsByCountry[countryId] || [];

  return (
    <div className="flex flex-col items-center space-y-4 p-6 w-full h-full">
      <h1 className="text-2xl font-bold">Choose your past</h1>
      <div className='flex gap-4 h-full'>
      {backgrounds.map((b) => (
        <div className='flex flex-col border border-rose-700 p-5 w-1/3 h-4/5 gap-2 justify-between'>
            <div>
                <h2 className="text-lg font-bold">{b.name}</h2>
                <p className="text-sm">{b.description}</p>
                <div className="list-disc list-inside p-2">
                    {b.buffs.map((buff) => (
                        <li className="text-sm text-green-500">{buff}</li>
                    ))}
                </div>
                <div className="list-disc list-inside p-2">
                    {b.debuffs.map((debuff) => (
                            <li className="text-sm text-red-500">{debuff}</li>
                    ))}
                </div>
            </div>
            <button
            key={b.id}
            className={`w-full px-4 py-2 border border-rose-700 hover:bg-rose-700 hover:cursor-pointer duration-150`}
            onClick={() => {
              setBackground(choosedBackground);
              router.push('/first-steps/three');
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
            onClick={() => router.push('/first-steps/one')}
        >
            Back
        </button>
      </div>

    </div>
  );
}
