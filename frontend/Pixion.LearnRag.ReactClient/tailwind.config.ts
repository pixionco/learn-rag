import { type Config } from "tailwindcss";
import reactAriaPlugin from "tailwindcss-react-aria-components";
import tailwindColors from "tailwindcss/colors";

export const customColors = {
  state: {
    invalid: tailwindColors.red["500"],
    active: tailwindColors.amber["500"],
    valid: tailwindColors.green["500"],
  },
  brand: {
    "50": "#F4F2FF",
    "100": "#EAE8FF",
    "200": "#D6D4FF",
    "300": "#BBB1FF",
    "400": "#9A85FF",
    "500": "#8664FF",
    "600": "#6830F7",
    "700": "#5A1EE3",
    "800": "#4B18BF",
    "900": "#3E169C",
    "950": "#260C6A",
  },
  neutral: {
    "50": "#F6F7F9",
    "100": "#ECEEF2",
    "200": "#D4D8E3",
    "300": "#ADB6CB",
    "400": "#818FAE",
    "500": "#627195",
    "600": "#4E5A7B",
    "700": "#404964",
    "800": "#373E55",
    "900": "#323748",
    "950": "#222430",
  },
};

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: customColors,
      keyframes: {
        slide: {
          "0%": { transform: "translateX(-100%)" },
          "100%": { transform: "translateX(0)" },
        },
      },
      animation: {
        "slide-in": "slide 300ms",
        "slide-out": "slide 300ms reverse ease-in",
      },
    },
  },
  plugins: [reactAriaPlugin],
} satisfies Config;
