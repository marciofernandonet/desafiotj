import { SyntheticEvent } from 'react';
import { IconButton, Snackbar, SnackbarCloseReason, Stack, Typography } from '@mui/material';
import IconifyIcon from 'components/base/IconifyIcon';

interface ProSnackbarProps {
  open: boolean;
  onClose: (event: SyntheticEvent | Event, reason?: SnackbarCloseReason) => void;
  message?: string;
  error?: boolean;
  duration?: number;
}

const mensageError = "Erro ao realizar operação";

const ProSnackbar = ({
  open,
  onClose,
  message = 'Operação realizada com sucesso!',
  error,
  duration = 3000,
}: ProSnackbarProps) => {
  return (
    <Snackbar
      open={open}
      onClose={onClose}
      autoHideDuration={duration}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'right',
      }}
      slotProps={{
        content: {
          sx: {
            backgroundColor: error ? "error.darker" : "info.darker"
          },
        },
      }}
      message={
        <Stack gap={1} alignItems="center">
          <Typography ml={2} variant="body2">{error ? mensageError : message}</Typography>
        </Stack>
      }
      action={
        <IconButton onClick={onClose} sx={{ mr: 1 }}>
          <IconifyIcon icon="material-symbols:close-rounded" sx={{ fontSize: 20 }} />
        </IconButton>
      }
    />
  );
};

export default ProSnackbar;
