import '@testing-library/jest-dom';
import { cleanup } from '@testing-library/react';
import { afterEach } from 'vitest';

// Runs cleanup after each test to unmount React trees
afterEach(() => {
    cleanup();
});