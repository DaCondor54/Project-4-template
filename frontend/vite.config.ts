import tailwindcss from '@tailwindcss/vite'
import react from '@vitejs/plugin-react'
import { resolve } from 'path'

/// <reference types="vitest/config" />
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
    plugins: [
        react({
            babel: {
                plugins: [['babel-plugin-react-compiler']],
            },
        }),
        tailwindcss()
    ],
    resolve: {
        alias: {
            '@': resolve(__dirname, './src'),
        },
    },
})
