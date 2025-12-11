import { defineConfig, mergeConfig } from 'vitest/config';
import viteConfig from './vite.config';

export default mergeConfig(viteConfig, defineConfig({
    test: {
        includeSource: ["src/**/*.{tsx,ts}"],
        globals: true,
        environment: 'jsdom',
        setupFiles: './src/setupTests.ts',
        coverage: {
            reporter: ["text", "html"],
            include: ["src/**/*.{tsx,ts}"]
        }
    }
}))
