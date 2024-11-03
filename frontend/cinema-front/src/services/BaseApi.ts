// src/services/BaseApi.ts

interface RequestOptions {
    headers?: { [key: string]: string };
    params?: { [key: string]: string | number };
  }
  
  /**
   * Uniwersalna funkcja GET do pobierania danych z backendu.
   * @param baseUrl - Bazowy adres URL API.
   * @param endpoint - Ścieżka końcowa dla żądania.
   * @param options - Opcje żądania, takie jak nagłówki i parametry.
   * @returns Promise z wynikiem typu `T`.
   */
  export const Get = async <T = any>(baseUrl: string, endpoint: string, options: RequestOptions = {}): Promise<T> => {
    try {
      // Budowanie pełnego URL-a z parametrami zapytania, jeśli istnieją
      const url = new URL(`${baseUrl}${endpoint}`);
      if (options.params) {
        Object.keys(options.params).forEach((key) =>
          url.searchParams.append(key, String(options.params![key]))
        );
      }
  
      const response = await fetch(url.toString(), {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          ...options.headers,
        },
      });
  
      if (!response.ok) {
        throw new Error(`Error ${response.status}: ${response.statusText}`);
      }
  
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error fetching data:', error);
      throw error;
    }
  };