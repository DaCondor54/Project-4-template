import { defineConfig } from 'vitest/config';

export default defineConfig({
    test: {
        includeSource: ["src/**/*.{tsx,ts}"],
        globals: true,
        environment: 'jsdom',
        setupFiles: './src/setupTests.ts',
        coverage: {
            reporter: ["text", "html"] 
        }
    }
})