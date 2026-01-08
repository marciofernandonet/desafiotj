import { useState } from 'react';
import Grid from '@mui/material/Grid';
import { useGridApiRef } from '@mui/x-data-grid';
import AuthorsTable from './AuthorsTable';

const UserListContainer = () => {
  const [filterButtonEl] = useState<HTMLButtonElement | null>(null);
  const apiRef = useGridApiRef();

  return (
    <Grid container spacing={4}>
      <Grid size={12}>
        <AuthorsTable apiRef={apiRef} filterButtonEl={filterButtonEl} />
      </Grid>
    </Grid>
  );
};

export default UserListContainer;
