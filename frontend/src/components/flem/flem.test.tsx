/* eslint-disable @typescript-eslint/no-explicit-any */


import * as useFlemHook from '@/hooks/useFlem';
import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import { Flem } from './flem.tsx';

vi.mock('@/hooks/useFlem');

const useFlemsSpy = vi.spyOn(useFlemHook, 'useFlems');
const useFlemAverageSpy = vi.spyOn(useFlemHook, 'useAverageFlem');
const useFlemSpy = vi.spyOn(useFlemHook, 'useFlem');

const loadingQuery = { data: undefined, isLoading: true, isError: false, isSuccess: false }
const sucessQuery = { data: 0.5, isLoading: false, isError: false, isSuccess: true }
const sucessQuery2 = { data: [ {flemRate: 0.5}], isLoading: false, isError: false, isSuccess: true }
const errorQuery = { data: undefined, error: new Error('Network error'), isLoading: false, isError: true, isSuccess: false }

describe('Flem', () => {
    it('should be loading', () => {
        useFlemSpy.mockReturnValue(loadingQuery as any);
        useFlemsSpy.mockReturnValue(loadingQuery as any);
        useFlemAverageSpy.mockReturnValue(loadingQuery as any);

        render(<Flem />);
        expect(screen.getByText("Loading...")).toBeInTheDocument();
    });

    it('should have succeeded', () => {
        useFlemSpy.mockReturnValue(sucessQuery as any);
        useFlemsSpy.mockReturnValue(sucessQuery2 as any);
        useFlemAverageSpy.mockReturnValue(sucessQuery as any);

        render(<Flem />);
        expect(screen.getByText("Gen Flem")).toBeInTheDocument();
    });

    it('should be an error', () => {
        useFlemSpy.mockReturnValue(errorQuery as any);
        useFlemsSpy.mockReturnValue(errorQuery as any);
        useFlemAverageSpy.mockReturnValue(errorQuery as any);

        render(<Flem />);
        expect(screen.getByText("Could Not get Flem Reason? Network error")).toBeInTheDocument();
    });

    afterEach(() => {
        vi.clearAllMocks();
    });
});

