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

// יצירת אקשן אסינכרוני לשינוי סיסמה
export const changePassword = createAsyncThunk<void, string>('auth/changePassword', async (newPassword: string) => {
    const response = await axios.put('https://localhost:7253/api/admin/myPassword', { password: newPassword }, {
        headers: {
            'Authorization': `Bearer ${sessionStorage.getItem('token')}`
        }
    });
    return response.data; // אם יש צורך, תוכל להחזיר נתונים נוספים
});

// יצירת ה-Slice
const authSlice = createSlice({
    name: 'auth',
    initialState: {
        token: null,
        error: null,
        loading: false,
    } as AuthState,
    reducers: {
        logout(state) {
            state.token = null;
            state.error = null;
            state.loading = false;
            sessionStorage.removeItem('token'); // מחיקת הטוקן מה-session storage
        },
    },
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
            })
            .addCase(changePassword.pending, (state) => {
                state.loading = true;
            })
            .addCase(changePassword.fulfilled, (state) => {
                state.loading = false;
                state.error = null; // תוכל להוסיף הודעת הצלחה אם תרצה
            })
            .addCase(changePassword.rejected, (state) => {
                state.loading = false;
                state.error = 'Password change failed';
            });
    },
});

// ייצוא של פעולות
export const { logout } = authSlice.actions;

export default authSlice.reducer;