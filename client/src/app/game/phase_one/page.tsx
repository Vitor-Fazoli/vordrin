
'use client'
import { useState } from "react";
import Tippy from '@tippyjs/react';
import 'tippy.js/dist/tippy.css';

export default function PhaseOne() {
    const [idCountry, setIdCountry] = useState<number>();

    function handleClick(id: number) {
        const element = document.getElementById(id.toString());
        resetCountries();

        if (element) {
            if (element.classList.contains("outline outline-rose-700 outline-offset-2")) {
                element.classList.remove("outline","outline-rose-700","outline-offset-2");
            } else {
                element.classList.add("outline","outline-rose-700","outline-offset-2");
            }
        }
        setIdCountry(id);
    }

    function resetCountries() {
        const elements = document.getElementsByClassName("pointer");
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.remove("outline","outline-rose-700","outline-offset-2");
        }
    }

    const locations = [
        { id: 1, name: "Brasil", top: "68%", left: "34%" },
        { id: 2, name: "United States", top: "47%", left: "20%" },
        { id: 3, name: "Austrália", top: "73%", left: "85%" },
        { id: 4, name: "Nippon", top: "47%", left: "86%" },
        { id: 5, name: "Misr", top: "52%", left: "56%" }
    ];

    return (
    <div className="flex flex-col items-center pt-4">
        <h1 className="text-md font-bold">Select a Country/Continent</h1>
        <div className="relative inline-block">
            <img 
                src="/worldmap.png"
                alt="Mapa Múndi"
                className="w-full max-w-3xl h-120"
            />
            {locations.map((loc, index) => (
                <Tippy content={loc.name}>
            <div id={loc.id.toString()}
                key={index}
                className="pointer absolute size-2 bg-rose-700 rounded-full cursor-pointer"
                style={{ top: loc.top, left: loc.left, transform: "translate(-50%, -50%)" }}
                onClick={() => handleClick(loc.id)}
            />
            </Tippy>
            ))}
        </div>
        <script>

        </script>
    </div>
    )
}
