'use client'

import { useState, useEffect } from 'react';
import ProgressBar from "./progress-bar";
import Image from "next/image";

interface PlayerStats {
    health: number;
    mana: number;
    attack: number;
    defense: number;
    level: number;
    experience: number;
    dodgeChance: number;
    dodgeCooldown: number;
}

const PlayerTab = () => {
    const [dodgeCooldown, setDodgeCooldown] = useState(2000);
    const [currentCooldown, setCurrentCooldown] = useState(0);
    const [canDodge, setCanDodge] = useState(true);

    const dodgeFunction = () => {
        if (!canDodge) return;

        document.getElementById("player")?.classList.add("mt-20");

        setTimeout(() => {
            document.getElementById("player")?.classList.remove("mt-20");
        }, 500);

        setCanDodge(false);
        setCurrentCooldown(dodgeCooldown);

        // Start cooldown countdown
        const startTime = Date.now();
        const cooldownInterval = setInterval(() => {
            const elapsed = Date.now() - startTime;
            const remaining = Math.max(0, dodgeCooldown - elapsed);
            setCurrentCooldown(remaining);

            if (remaining === 0) {
                setCanDodge(true);
                clearInterval(cooldownInterval);
            }
        }, 50);
    };

    useEffect(() => {
        const handleKeyDown = (event: KeyboardEvent) => {
            switch (event.key) {
                case "Shift": // Shift pode ser usado para desviar
                    dodgeFunction();
                    break;
                default:
                    break;
            }
        };

        window.addEventListener("keydown", handleKeyDown);

        return () => {
            window.removeEventListener("keydown", handleKeyDown);
        };
    }, [canDodge]); // Add canDodge to dependency array

    return (
        <div id='player' className='flex gap-1 duration-500'>
            <div className='flex flex-col gap-1'>
                <div className="border border-rose-900/50 p-2  ">
                    <h2><span className="text-rose-700 outline mr-0.5 font-bold">O</span>ilmann</h2>
                    <div className="flex gap-10">
                        <div>
                            <p>Health</p>
                            <ProgressBar progress={100} maxValue={100} small={true}></ProgressBar>
                            <p>Shield</p>
                            <ProgressBar progress={0} maxValue={100} small={true}></ProgressBar>
                        </div>
                    </div>
                </div>
                <ProgressBar
                    progress={dodgeCooldown - currentCooldown}
                    maxValue={dodgeCooldown}
                    small={true}
                ></ProgressBar>
            </div>
            <div className="border border-rose-900/50 w-5">
            </div>
        </div>
    );
};

export default PlayerTab;
