'use client'

import { useState } from "react";
import { FaPlus, FaMinus } from "react-icons/fa6";

const MAX_ATTRIBUTTES : number = 10;

const initialAttributtes = [
    {
      id: 1,
      name: "Ferocity",
      description: "Increases physical damage and brute force of attacks.",
      value: 2
    },
    {
      id: 2,
      name: "Cunning",
      description: "Enhances accuracy and the chance of landing precise strikes.",
      value: 2
    },
    {
      id: 3,
      name: "Fortitude",
      description: "Boosts resistance to damage and overall durability.",
      value: 2
    },
    {
      id: 4,
      name: "Instinct",
      description: "Reduces cooldown times and sharpens reflexes.",
      value: 2
    }
];

export default function firstStepPage() {

    const [attributtes, setAttributtes ] = useState(initialAttributtes);
    
    function increaseAttribute(id: number) {
        const totalValue = attributtes.reduce((sum, attribute) => sum + attribute.value, 0);

        if (totalValue >= MAX_ATTRIBUTTES) {
            alert("You cannot allocate more than 10 points.");
            return;
        }

        const updatedAttributes = attributtes.map(attribute =>
            attribute.id === id ? { ...attribute, value: attribute.value + 1 } : attribute
        );
        setAttributtes(updatedAttributes);
    }

    function decreaseAttribute(id: number) {
        const updatedAttributes = attributtes.map(attribute => 
            attribute.id === id && attribute.value > 1 
                ? { ...attribute, value: attribute.value - 1 } 
                : attribute
        );
        setAttributtes(updatedAttributes);
    }


    return (
        <div className='h-screen w-screen'>
            <div className="h-full w-full flex p-10 gap-5">
                <div className="h-full w-1/3 border border-rose-700 p-2">
                    <h1 className="text-xl">Attributtes</h1>
                    <ul className="list rounded-box shadow-md">
                        {attributtes.map((attribute) => (
                            <li key={attribute.id} className="list-row flex justify-between">
                                <div className="w-3/5">
                                    <h1 className="text-sm uppercase font-semibold">{attribute.name}</h1>
                                    <p className="text-xs opacity-50">{attribute.description}</p>
                                </div>
                                <div className="flex justify-between items-center">
                                    <button onClick={() => {decreaseAttribute(attribute.id)}} className="btn btn-xs btn-square btn-ghost">
                                        <FaMinus className="text-rose-700" />
                                    </button>
                                    <button onClick={() => {increaseAttribute(attribute.id)}} className="btn btn-xs btn-square btn-ghost">
                                        <FaPlus className="text-rose-700" />
                                    </button>
                                    <h1 className="w-10 text-center">{attribute.value}</h1>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
                <div className="flex flex-col h-full w-2/3 border border-rose-700 p-2 gap-4">
                        <input type="text" className="border border-rose-700" placeholder="Name"/>
                        <div className="flex gap-4">
                            <p>
                                Greatsword
                            </p>
                            <input type="radio" name="radio-weapon" className="radio border border-rose-700" checked/>
                            <p>
                                Bow
                            </p>
                            <input type="radio" name="radio-weapon" className="radio border border-rose-700" />
                            <p>
                                Purger
                            </p>
                            <input type="radio" name="radio-weapon" className="radio border border-rose-700" />
                        </div>
                </div>
            </div>
        </div>
    );
}