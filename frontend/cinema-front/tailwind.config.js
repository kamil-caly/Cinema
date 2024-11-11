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
        cMainBgHover: "#2d2d59",
        cinemaYellow: "#fdeb8f"
      },
      height: {
        'movieImage': '32rem',
      },
      width: {
        'seanceCard': '802px'
      },
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
      },
    },
  },
  plugins: [],
}

