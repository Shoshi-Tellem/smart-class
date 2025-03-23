import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import axios from 'axios';
import { Login } from '../extensions/entities';

interface AuthState {
    token: string | null;
    error: string | null;
    loading: boolean;
}

// יצירת אקשן אסינכרוני להתחברות
export const loginUser = createAsyncThunk<string, Login>('auth/loginUser', async (loginData: Login) => {
    const response = await axios.post('https://localhost:7253/api/auth', loginData);
    return response.data.token;
});

// יצירת ה-Slice
const authSlice = createSlice({
    name: 'auth',
    initialState: {
        token: null,
        error: null,
        loading: false,
    } as AuthState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(loginUser.pending, (state) => {
                state.loading = true;
            })
            .addCase(loginUser.fulfilled, (state, action: PayloadAction<string>) => {
                state.loading = false;
                state.token = action.payload;
                state.error = null;

                // שמירת הטוקן ב-session storage
                sessionStorage.setItem('token', action.payload);
            })
            .addCase(loginUser.rejected, (state) => {
                state.loading = false;
                state.error = 'Login failed';
            });
    },
});

export default authSlice.reducer;