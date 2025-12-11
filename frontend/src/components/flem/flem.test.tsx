/* eslint-disable @typescript-eslint/no-explicit-any */


import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import * as useFlemHook from '../../hooks/useFlem';
import { Flem } from './flem.tsx';

vi.mock('../../hooks/useFlem');

const useFlemSpy = vi.spyOn(useFlemHook, 'useFlem');

const loadingQuery = { data: undefined, isLoading: true, isError: false, isSuccess: false }
const sucessQuery = { data: {data: {reason: 'flem'}}, isLoading: false, isError: false, isSuccess: true }
const errorQuery = { data: undefined, error: new Error('Network error'), isLoading: false, isError: true, isSuccess: false }

describe('Flem', () => {
    it('should be loading', () => {
        useFlemSpy.mockReturnValue(loadingQuery as any);

        render(<Flem />);
        expect(screen.getByText("Loading...")).toBeInTheDocument();
    });

    it('should have succeeded', () => {
        useFlemSpy.mockReturnValue(sucessQuery as any);

        render(<Flem />);
        expect(screen.getByText("Hello World! : flem")).toBeInTheDocument();
    });

    it('should be an error', () => {
        useFlemSpy.mockReturnValue(errorQuery as any);

        render(<Flem />);
        expect(screen.getByText("Could Not get Flem Reason? Network error")).toBeInTheDocument();
    });

    afterEach(() => {
        vi.clearAllMocks();
    });
});

