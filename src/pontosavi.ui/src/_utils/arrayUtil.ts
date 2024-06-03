/**
 * Determines whether an array includes a given array's elements, returning true or false as appropriate.
 * @param searchElements The elements to search for.
 */
export const includes = <T>(array: T[], searchElements: T[]): boolean =>
    searchElements.some(searchElement => array.includes(searchElement));