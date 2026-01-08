import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import AuthorListContainer from 'components/sections/author-table';
import PageHeader from 'components/sections/author-table/PageHeader';

const AuthorList = () => {
  return (
    <Stack direction="column" height={1}>
      <PageHeader
        title="Autores"
        breadcrumb={[]}
      />
      <Paper sx={{ flex: 1, p: { xs: 3, md: 5 } }}>
        <AuthorListContainer />
      </Paper>
    </Stack>
  );
};

export default AuthorList;
