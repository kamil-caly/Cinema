export const useLocalStorage = () => {
    const setValue = <T>(key: string, value: T) => {
        localStorage.setItem(key, JSON.stringify(value));
    };

    const getValue = <T>(key: string): T | null => {
        const item: string | null = localStorage.getItem(key);
        return item ? JSON.parse(item) : null;
    };

    return [getValue, setValue] as const;
}