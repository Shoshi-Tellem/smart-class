// import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
// import axios from 'axios';
// import { Login } from '../extensions/entities';

// interface AuthState {
//   token: string | null;
//   error: string | null;
//   loading: boolean;
// }

// interface LoginResponse {
//   token: string;
//   role: string;
//   passwordChanged: boolean;
// }

// // אקשן אסינכרוני להתחברות
// export const loginUser = createAsyncThunk<LoginResponse, Login>(
//   'auth/loginUser',
//   async (loginData: Login) => {
//     const response = await axios.post<LoginResponse>('https://localhost:7253/api/auth', loginData);
//     sessionStorage.setItem('token', response.data.token);
//     sessionStorage.setItem('role', response.data.role);
//     return response.data;
//   }
// );

// // אקשן אסינכרוני לשינוי סיסמה
// export const changePassword = createAsyncThunk<void, string>(
//   'auth/changePassword',
//   async (newPassword: string, { rejectWithValue }) => {
//     const role = sessionStorage.getItem('role');
//     const token = sessionStorage.getItem('token');

//     if (!role || !token) {
//       return rejectWithValue("Role or token is missing");
//     }

//     try {
//       await axios.put(
//         `https://localhost:7253/api/${role}/me`,
//         { password: newPassword },
//         {
//           headers: {
//             Authorization: `Bearer ${token}`,
//             'Content-Type': 'application/json',
//           },
//         }
//       );
//     } catch (err: any) {
//       return rejectWithValue(err.response?.data || err.message);
//     }
//   }
// );

// // Slice
// const authSlice = createSlice({
//   name: 'auth',
//   initialState: {
//     token: null,
//     error: null,
//     loading: false,
//   } as AuthState,
//   reducers: {
//     logout(state) {
//       state.token = null;
//       state.error = null;
//       state.loading = false;
//       sessionStorage.removeItem('token');
//       sessionStorage.removeItem('role');
//     },
//   },
//   extraReducers: (builder) => {
//     builder
//       .addCase(loginUser.pending, (state) => {
//         state.loading = true;
//         state.error = null;
//       })
//       .addCase(loginUser.fulfilled, (state, action: PayloadAction<LoginResponse>) => {
//         state.loading = false;
//         state.token = action.payload.token;
//       })
//       .addCase(loginUser.rejected, (state) => {
//         state.loading = false;
//         state.error = 'Login failed';
//       })
//       .addCase(changePassword.pending, (state) => {
//         state.loading = true;
//         state.error = null;
//       })
//       .addCase(changePassword.fulfilled, (state) => {
//         state.loading = false;
//       })
//       .addCase(changePassword.rejected, (state, action) => {
//         state.loading = false;
//         state.error = action.payload as string || 'Password change failed';
//       });
//   },
// });

// export const { logout } = authSlice.actions;
// export default authSlice.reducer;

import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit'
import apiClient from '../extensions/apiService'
import { Login } from '../extensions/entities'

interface AuthState {
  token: string | null;
  error: string | null;
  loading: boolean;
}

interface LoginResponse {
  token: string;
  role: string;
  passwordChanged: boolean;
}

// אקשן אסינכרוני להתחברות
export const loginUser = createAsyncThunk<LoginResponse, Login>(
  'auth/loginUser',
  async (loginData: Login) => {
    const response = await apiClient.post<LoginResponse>('/auth', loginData)
    sessionStorage.setItem('token', response.data.token);
    sessionStorage.setItem('role', response.data.role);
    return response.data;
  }
);

// אקשן אסינכרוני לשינוי סיסמה
export const changePassword = createAsyncThunk<void, string>(
  'auth/changePassword',
  async (newPassword: string, { rejectWithValue }) => {
    const role = sessionStorage.getItem('role');
    const token = sessionStorage.getItem('token');

    if (!role || !token) {
      return rejectWithValue("Role or token is missing");
    }

    try {
      await apiClient.put(
        `/${role}/me`,
        { password: newPassword }
      );
    } catch (err: any) {
      return rejectWithValue(err.response?.data || err.message);
    }
  }
);

// Slice
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
      sessionStorage.removeItem('token');
      sessionStorage.removeItem('role');
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loginUser.fulfilled, (state, action: PayloadAction<LoginResponse>) => {
        state.loading = false;
        state.token = action.payload.token;
      })
      .addCase(loginUser.rejected, (state) => {
        state.loading = false;
        state.error = 'Login failed';
      })
      .addCase(changePassword.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(changePassword.fulfilled, (state) => {
        state.loading = false;
      })
      .addCase(changePassword.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string || 'Password change failed';
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;