import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { File } from '../extensions/entities'; // ודא שהסוג מוגדר כאן

// URL של ה-API
const API_URL = 'http://localhost:5000/api/file'; // עדכן את הכתובת לפי הצורך

// Async thunk to fetch files
export const fetchFiles = createAsyncThunk<File[], void>('files/fetchFiles', async () => {
    const response = await axios.get<File[]>(API_URL);
    return response.data;
});

// Async thunk to add a file
export const addFile = createAsyncThunk<File, File>('files/addFile', async (file: File) => {
    const response = await axios.post<File>(API_URL, file);
    return response.data;
});

// Async thunk to update a file
export const updateFile = createAsyncThunk<File, { id: number; file: File }>(
    'files/updateFile',
    async ({ id, file }: { id: number; file: File }) => {
        const response = await axios.put<File>(`${API_URL}/${id}`, file);
        return response.data;
    }
);

// Async thunk to delete a file
export const deleteFile = createAsyncThunk<File, number>('files/deleteFile', async (id: number) => {
    const response = await axios.delete<File>(`${API_URL}/${id}`);
    return response.data;
});

// Create a slice for files
const fileSlice = createSlice({
    name: 'files',
    initialState: {
        files: [] as File[],
        loading: false,
        error: null as string | null,
    },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchFiles.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchFiles.fulfilled, (state, action) => {
                state.loading = false;
                state.files = action.payload;
            })
            .addCase(fetchFiles.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Error fetching files';
            })
            .addCase(addFile.fulfilled, (state, action) => {
                state.files.push(action.payload);
            })
            .addCase(updateFile.fulfilled, (state, action) => {
                const index = state.files.findIndex(file => file.id === action.payload.id);
                if (index !== -1) {
                    state.files[index] = action.payload;
                }
            })
            .addCase(deleteFile.fulfilled, (state, action) => {
                state.files = state.files.filter(file => file.id !== action.payload.id);
            });
    },
});

// Export the reducer to be used in the store
export default fileSlice.reducer;