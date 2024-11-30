/** @type {import('tailwindcss').Config} */
module.exports = {
	content: [
		"./src/**/*.{js,jsx,ts,tsx}",
	],
	theme: {
		extend: {
			colors: {
				// Bg
				cinemaBgPrimary: '#23272f',
				cinemaBgSecondary: '#333a45',
				cinemaBgTertiary: '#232730',
				cinemaBgGreen: '#25353a',
				cinemaBgViolet: '#252943',
				cinemaBgOrange: '#403026',
				cinemaBgRed: '#33282e',

				// Txt
				cinemaTextPrimary: '#f6f7f9',
				cinemaTextSecondary: '#5acbe3',
				cinemaTextGrayStrong: '#9ca3af',
				cinemaTextGrayLight: '#d1d5db',
				cinemaTextViolet: '#8891ec',
				cinemaTextGreen: '#44ac99',
				cinemaTextOrange: '#db7d27',
				cinemaTextRed: '#c1554d',

				// Btn
				cinemaBtnPrimary: '#58c4dc',
				cinemaBtnSecondary: '#252932',
				cinemaBtnViolet: '#575fb7',
				cinemaBtnVioletHover: '#6b75db',

				// Hover
				cinemaHoverPrimary: '#2e323a',
				cinemaHoverSecondary: '#293541',

				// Border
				cinemaBorderPrimary: '#434b5a',
				cinemaBorderSecondary: '#30333a',

				// Colors
				cinemaBlack: '#000000',

				// WebPage ScrollBar
				cinemaBgScrollBar: '#424242',
				cinemaScrollBar: '#686868',
			},
			height: {
				'navbar': '72px',
				'movieImage': '32rem',
			},
			width: {
				'seanceCard': '802px',
				'accountForm': '550px'
			},
			padding: {
				'pageTopPadding': '104px'
			},
			backgroundImage: {
				'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
			},
		},
	},
	plugins: [],
}

