import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import BookListContainer from 'components/sections/book-table';
import PageHeader from 'components/sections/book-table/PageHeader';

const BookList = () => {
  return (
    <Stack direction="column" height={1}>
      <PageHeader
        title="Livros"
        breadcrumb={[]}
      />
      <Paper sx={{ flex: 1, p: { xs: 3, md: 5 } }}>
        <BookListContainer />
      </Paper>
    </Stack>
  );
};

export default BookList;
