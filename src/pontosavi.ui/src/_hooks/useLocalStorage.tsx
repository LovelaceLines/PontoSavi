"use client";

import { useState, useEffect } from "react";

import { getKey, getStorageValue } from "@/_services";

/**
 * Custom hook for managing values in local storage.
 * 
 * @param key - The key to use for storing the value in local storage.
 * @param initialValue - The initial value to use if no value is found in local storage. Defaults to an empty string.
 * @returns A tuple containing the current value and a function to update the value.
 */
export const useLocalStorage = (key: string, initialValue?: unknown) => {
  key = getKey(key);

  const [value, setValue] = useState(getStorageValue(key, initialValue));

  useEffect(() => {
    localStorage.setItem(key, JSON.stringify(value));
  }, [key, value]);

  return [value, setValue];
};
