import { create } from 'zustand';

interface FirstStepsState {
  countryId: number | null;
  backgroundId: number | null;
  weaponId: number | null;
  setCountry: (id: number) => void;
  setBackground: (id: number) => void;
  setWeapon: (id: number) => void;
  reset: () => void;
}

export const useFirstStepsStore = create<FirstStepsState>((set: (arg0: { countryId?: any; backgroundId?: any; weaponId?: any; }) => any) => ({
  countryId: null,
  backgroundId: null,
  weaponId: null,
  setCountry: (id: number) => set({ countryId: id }),
  setBackground: (id: number) => set({ backgroundId: id }),
  setWeapon: (id: number) => set({ weaponId: id }),
  reset: () => set({ countryId: null, backgroundId: null, weaponId: null }),
}));
