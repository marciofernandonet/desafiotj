import { useState } from 'react';
import Grid from '@mui/material/Grid';
import { useGridApiRef } from '@mui/x-data-grid';
import SubjectsTable from './SubjectsTable';

const SubjectListContainer = () => {
  const [filterButtonEl] = useState<HTMLButtonElement | null>(null);
  const apiRef = useGridApiRef();

  return (
    <Grid container spacing={4}>
      <Grid size={12}>
        <SubjectsTable apiRef={apiRef} filterButtonEl={filterButtonEl} />
      </Grid>
    </Grid>
  );
};

export default SubjectListContainer;
