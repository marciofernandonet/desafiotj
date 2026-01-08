import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import SubjectListContainer from 'components/sections/subject-table';
import PageHeader from 'components/sections/book-table/PageHeader';

const SubjectList = () => {
  return (
    <Stack direction="column" height={1}>
      <PageHeader
        title="Assuntos"
        breadcrumb={[]}
      />
      <Paper sx={{ flex: 1, p: { xs: 3, md: 5 } }}>
        <SubjectListContainer />
      </Paper>
    </Stack>
  );
};

export default SubjectList;
