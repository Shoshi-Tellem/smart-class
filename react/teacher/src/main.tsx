import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux'; // הוסף את ה-import הזה
import store from './stores/store'; // ודא שהנתיב נכון ל-store שלך
import './index.css';
import App from './App.tsx';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}> {/* עטוף את App ב-Provider */}
      <App />
    </Provider>
  </StrictMode>,
);