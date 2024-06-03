"use client";

export const getKey = (key: string): string => `@pontosavi:${key}`;

export const getStorageValue = (key: string, defaultValue?: unknown) => {
  if (typeof window === "undefined") return defaultValue || null;

  const item = localStorage.getItem(getKey(key));

  return item ? JSON.parse(item) : defaultValue || null;
};

export const setStorageValue = (key: string, value: unknown) => {
  if (typeof window === "undefined") return;

  localStorage.setItem(getKey(key), JSON.stringify(value));

  return value;
};