// src/services/BaseApi.ts

interface RequestOptions {
	headers?: { [key: string]: string };
	params?: { [key: string]: string | number };
	body?: any;
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
		const token: string = localStorage.getItem('token') ?? '';
		const parsedToken: string = token ? JSON.parse(token) : '';

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
				'Authorization': `Bearer ${parsedToken}`,
				...options.headers,
			},
		});

		if (!response.ok) {
			const errorBody = await response.json().catch(() => null);
			throw {
				status: response.status,
				statusText: response.statusText,
				url: response.url,
				body: errorBody,
			};
		}

		const data = await response.json();
		return data;
	} catch (error) {
		console.error('Error fetching data:', error);
		throw error;
	}
};

/**
* Uniwersalna funkcja POST do wysyłania danych do backendu.
* @param baseUrl - Bazowy adres URL API.
* @param endpoint - Ścieżka końcowa dla żądania.
* @param options - Opcje żądania, takie jak nagłówki, parametry i ciało.
* @returns Promise z wynikiem typu `T`.
*/
export const Post = async <T = any>(
	baseUrl: string,
	endpoint: string,
	options: RequestOptions = {}
): Promise<T> => {
	try {
		debugger;
		const token: string = localStorage.getItem('token') ?? '';
		const parsedToken: string = token ? JSON.parse(token) : '';

		const url = new URL(`${baseUrl}${endpoint}`);
		if (options.params) {
			Object.keys(options.params).forEach((key) =>
				url.searchParams.append(key, String(options.params![key]))
			);
		}

		const response = await fetch(url.toString(), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				'Authorization': `Bearer ${parsedToken}`,
				...options.headers,
			},
			body: JSON.stringify(options.body),
		});

		if (!response.ok) {
			const errorBody = await response.json().catch(() => null);
			throw {
				status: response.status,
				statusText: response.statusText,
				url: response.url,
				body: errorBody,
			};
		}

		const contentType = response.headers.get('Content-Type') || '';

		if (contentType.includes('application/json')) {
			return await response.json();
		} else {
			return (await response.text()) as unknown as T;
		}
	} catch (error) {
		console.error('Error posting data:', error);
		throw error;
	}
};

/**
 * Uniwersalna funkcja PUT do aktualizowania danych w backendzie.
 * @param baseUrl - Bazowy adres URL API.
 * @param endpoint - Ścieżka końcowa dla żądania.
 * @param options - Opcje żądania, takie jak nagłówki, parametry i ciało.
 * @returns Promise z wynikiem typu `T`.
 */
export const Put = async <T = any>(
	baseUrl: string,
	endpoint: string,
	options: RequestOptions = {}
): Promise<T> => {
	try {
		const token: string = localStorage.getItem('token') ?? '';
		const parsedToken: string = token ? JSON.parse(token) : '';

		const url = new URL(`${baseUrl}${endpoint}`);
		if (options.params) {
			Object.keys(options.params).forEach((key) =>
				url.searchParams.append(key, String(options.params![key]))
			);
		}

		const response = await fetch(url.toString(), {
			method: 'PUT',
			headers: {
				'Content-Type': 'application/json',
				'Authorization': `Bearer ${parsedToken}`,
				...options.headers,
			},
			body: JSON.stringify(options.body),
		});

		if (!response.ok) {
			const errorBody = await response.json().catch(() => null);
			throw {
				status: response.status,
				statusText: response.statusText,
				url: response.url,
				body: errorBody,
			};
		}

		const contentType = response.headers.get('Content-Type') || '';

		if (contentType.includes('application/json')) {
			return await response.json();
		} else {
			return (await response.text()) as unknown as T;
		}
	} catch (error) {
		console.error('Error updating data:', error);
		throw error;
	}
};
