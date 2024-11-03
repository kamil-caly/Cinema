/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        cMainBg: "#1a1a2e",       // Tło navbaru
        cMainText: "#e0e0e0",     // Kolor tekstu
        cMainTextHover: "#ff6347",    // Kolor tekstu po najechaniu
        cinemaBg: '#030712',
        cMainBhHover: "#2d2d59",
      },
      height: {
        'movieImage': '32rem',
      },
    },
  },
  plugins: [],
}

